﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GurpsCompanion.Shared {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ApiAddressResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ApiAddressResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GurpsCompanion.Shared.ApiAddressResources", typeof(ApiAddressResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /api/character?hash={0}&amp;salt={1}.
        /// </summary>
        public static string Character_Base {
            get {
                return ResourceManager.GetString("Character_Base", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /api/character/characterinformation?id={0}.
        /// </summary>
        public static string GetCharacterInformation {
            get {
                return ResourceManager.GetString("GetCharacterInformation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /api/item/item?name={0}.
        /// </summary>
        public static string GetItem {
            get {
                return ResourceManager.GetString("GetItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /api/item/itemnames.
        /// </summary>
        public static string GetItemNames {
            get {
                return ResourceManager.GetString("GetItemNames", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /api/item.
        /// </summary>
        public static string Item_Base {
            get {
                return ResourceManager.GetString("Item_Base", resourceCulture);
            }
        }
    }
}
