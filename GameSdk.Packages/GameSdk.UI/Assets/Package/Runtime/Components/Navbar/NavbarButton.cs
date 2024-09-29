using System;
using UnityEngine.UIElements;

namespace GameSdk.UI
{
    [UxmlElement("NavbarButton")]
    public partial class NavbarButton : Button
    {
        [UxmlAttribute, UxmlTypeReference(typeof(Screen))]
        public Type TargetScreen { get; set; }
    }
}
