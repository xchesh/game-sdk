#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Core.Common.Toolbox
{
    /// <summary>
    /// Nest all animation clips in an animator controller.
    /// </summary>
    public class AnimationNestClips : MonoBehaviour
    {
        [MenuItem("Assets/Nest Animation Clips in Controller", true)]
        public static bool NestAnimationClipsValidate()
        {
            return Selection.activeObject is AnimatorController;
        }

        [MenuItem("Assets/Nest Animation Clips in Controller")]
        public static void NestAnimationClips()
        {
            var animController = (AnimatorController)Selection.activeObject;
            var objects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(animController));

            if (animController != null)
            {
                AssetDatabase.SaveAssets();
                var states = new List<ChildAnimatorState>();
                var layers = animController.layers;

                foreach (var layer in layers)
                {
                    states.AddRange(layer.stateMachine.states.ToList<ChildAnimatorState>());
                }

                if (states.Count > 0)
                {
                    for (var i = 0; i < states.Count; i++)
                    {
                        if (states[i].state.motion)
                        {
                            var newClip = Instantiate((AnimationClip)states[i].state.motion) as AnimationClip;
                            newClip.name = states[i].state.motion.name;
                            AssetDatabase.AddObjectToAsset(newClip, animController);
                            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newClip));
                            states[i].state.motion = newClip;
                        }
                    }
                }
            }

            for (var i = 0; i < objects.Length; i++)
            {
                if (objects[i] is AnimationClip)
                {
                    DestroyImmediate(objects[i], true);
                }
            }

            AssetDatabase.SaveAssets();
        }
    }
}
#endif
