using ProfileSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfileSample.DAL
{
    public class ProfileSampleEntitiesRepo
    {
        private readonly ProfileSampleEntities context;
        public ProfileSampleEntitiesRepo()
        {
            context = new ProfileSampleEntities();
        }
        public List<ImageModel> GetImages()
        {

            var sources = context.ImgSources.
                Take(20).Select( x => new ImageModel {
                Name = x.Name,
                Data = x.Data
            });

            return sources.ToList<ImageModel>();
        }
    }
}