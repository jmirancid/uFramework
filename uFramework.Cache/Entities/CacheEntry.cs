using System;
using System.ComponentModel.DataAnnotations;
using uFramework.Cache.Resources;
using uFramework.Entities.Resources;

namespace uFramework.Cache.Entities
{
    [MetadataType(typeof(CacheEntry_Metadata))]
    public class CacheEntry
    {
        public string Key { get; set; }

        public object Value { get; set; }

        public DateTime Expiry { get; set; }

        public bool Sliding { get; set; }
    }

    [Serializable]
    public class CacheEntry_Metadata
    {
        [Display(Name = "Entity_Key", ResourceType = typeof(Ent_CacheEntryResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Ent_ValidationResource))]
        public string Key { get; set; }

        [Display(Name = "Entity_Policy", ResourceType = typeof(Ent_CacheEntryResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Ent_ValidationResource))]
        public object Value { get; set; }

        [Display(Name = "Entity_Expiry", ResourceType = typeof(Ent_CacheEntryResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Ent_ValidationResource))]
        public DateTime Expiry { get; set; }

        [Display(Name = "Entity_Sliding", ResourceType = typeof(Ent_CacheEntryResource))]
        public bool Sliding { get; set; }
    }
}
