using System;
using UnityEngine.UIElements;
using Navigation;

namespace Navbar
{
    [UxmlElement("NavbarButton")]
    public partial class NavbarButtonElement : Button
    {
        [UxmlAttribute, UxmlTypeReference(typeof(ScreenElement))]
        public Type TargetScreen { get; set; }
    }
}
