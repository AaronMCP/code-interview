using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;


namespace Hys.CareAgent.DAP
{
    public class Startup
    {
        public static void AutoMapperStart()
        {
            var profiles = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(Profile).IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null)
                .Select(Activator.CreateInstance).Cast<Profile>().ToList();
            foreach (var profile in profiles)
            {
                Mapper.AddProfile(profile);
            }

        }
    }
}
