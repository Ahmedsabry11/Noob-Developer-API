using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModels;
using DomainLayer.Entities;
using DomainLayer.Data;
using RepositoryLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using WebTool.Services.IServices;

namespace WebTool.Services.Services
{
    public class PropertyServices: IPropertyServices
    {
        private readonly IRepository<Property> _PropertyRepository;
        private readonly APPDBContext _context;

        public PropertyServices(IRepository<Property> PropertyRepository, APPDBContext context)
        {
            _PropertyRepository = PropertyRepository ??
                                  throw new ArgumentNullException(nameof(PropertyRepository));
            _context = context ??
                       throw new ArgumentNullException(nameof(context));
        }

        //public async Task<Dictionary<string, List<string>>> GetWidgetStyle(int id)
        //{
        //    //Console.WriteLine("\n\n##################################\n\n");
        //    // V
        //    var vaildwidgetid = _context.Widgets.Where(x => x.Id == id).FirstOrDefault();
        //    if(vaildwidgetid == null)
        //    {
        //        return null;
        //    }
        //    var properties = await (from p in _context.Properties
        //            join wp in _context.WidgetProperties
        //                on p.Id equals wp.Property.Id
        //            join pv in _context.PropertyValues
        //                on p.Id equals pv.PropertyID
        //            where wp.WidgetID == id
        //            orderby p.Id ascending
        //            select new
        //            {
        //                Id = p.Id,
        //                PropertyName = p.PropertyName,
        //                ParentPropertyID = p.ParentPropertyID,
        //                Description = p.Description,
        //                // different
        //                DefaultValue = wp.DefaultValue,
        //                Value = pv.Value,
        //                IsDefaultPV = pv.IsDefault
        //            }
        //        ).ToListAsync();
        //    //// U
        //    //var PUnits = await (from p in _context.Properties
        //    //        join wp in _context.WidgetProperties
        //    //            on p.Id equals wp.Property.Id
        //    //        join pu in _context.PropertyUnits
        //    //            on p.Id equals pu.PropertyID
        //    //        join u in _context.Units
        //    //            on pu.UnitID equals u.Id
        //    //        where wp.WidgetID == id
        //    //        orderby p.Id ascending
        //    //        select new
        //    //        {
        //    //            Id = p.Id,
        //    //            PropertyName = p.PropertyName,
        //    //            ParentPropertyID = p.ParentPropertyID,
        //    //            Description = p.Description,
        //    //            // different
        //    //            DefaultValue = wp.DefaultValue,
        //    //            isDefaultPU = pu.isDefault,
        //    //            UnitName = u.UnitName,
        //    //            isDefaultU = u.isDefault
        //    //        }
        //    //    ).ToListAsync();

        //    if (properties.Count == 0)
        //        return new Dictionary<string, List<string>>(); ;
        //    // ------------------------- should use auto-mapper ---------------------------
        //    List<PropertyDTO> result = new List<PropertyDTO>();
        //    Dictionary<string, List<string>> mp = new Dictionary<string, List<string>>();
        //    foreach (var property in properties)
        //    {
        //        result.Add(new PropertyDTO
        //        {
        //            Id = property.Id,
        //            Description = property.Description,
        //            IsDefault = property.IsDefaultPV,
        //            ParentPropertyID = property.ParentPropertyID,
        //            PropertyName = property.PropertyName,
        //            //diff
        //            Value = property.Value,
        //            //DefaultValue = property.DefaultValue,
        //        });
        //        string propertyName = property.PropertyName; //
        //        if (!mp.ContainsKey(property.PropertyName))
        //            mp[property.PropertyName] = new List<string>();
        //        mp[property.PropertyName].Add(property.Value);

        //    }

        //    //foreach (var key in mp)
        //    //{
        //    //    Console.WriteLine(key);
        //    //    foreach (var val in key.Value)
        //    //    {
        //    //        Console.WriteLine(val+" ");
        //    //    }
        //    //    Console.WriteLine("\n");
        //    //}
        //    return mp;
        //}
        public async Task<List<PropertyDTO>> TestStyles(int id)
        {
            var vaildwidgetid = _context.Widgets.Where(x => x.Id == id).FirstOrDefault();
            if (vaildwidgetid == null)
            {
                return null;
            }
            var allProperties = await (from p in _context.Properties
                                       join wp in _context.WidgetProperties
                                       on p.Id equals wp.Property.Id
                                       where wp.WidgetID == id
                                       orderby p.Id ascending
                                       select new
                                       {
                                           Id = p.Id,
                                           PropertyName = p.PropertyName,
                                           ParentPropertyID = p.ParentPropertyID,
                                           Description = p.Description,
                                           DefaultValue = wp.DefaultValue
                                       }
                ).ToListAsync();
            int noProperities = allProperties.Count();
            Dictionary<int,PropertyDTO> WidegtProperites = new Dictionary<int,PropertyDTO>();
            foreach (var property in allProperties)
            {
                WidegtProperites.Add(property.Id,new PropertyDTO
                {
                    Id = property.Id,
                    Description = property.Description,
                    ParentPropertyID = property.ParentPropertyID,
                    PropertyName = property.PropertyName,
                    DefaultValue = property.DefaultValue

                });

            }

            var PropertiesWithValues = await (from p in _context.Properties
                                              join wp in _context.WidgetProperties
                                              on p.Id equals wp.Property.Id
                                              join pv in _context.PropertyValues
                                              on p.Id equals pv.PropertyID
                                              where wp.WidgetID == id
                                              orderby p.Id ascending
                                              select new
                                              {
                                                  Id = pv.Id,
                                                  Value = pv.Value,
                                                  IsDefault = pv.IsDefault,
                                                  PropertyID = p.Id,
                                              }
                                              ).ToListAsync();
            Console.WriteLine("Start Coping Values to each property");
            foreach(var propertyvalue in PropertiesWithValues)
            {
                int propertyid = propertyvalue.PropertyID;
                if(WidegtProperites[propertyid].Values == null)
                {
                    WidegtProperites[propertyid].Values = new List<PropertyValueDTO>();
                    WidegtProperites[propertyid].Values.Add(new PropertyValueDTO
                    {
                        Id = propertyvalue.Id,
                        isDefault = propertyvalue.IsDefault,
                        value = propertyvalue.Value,
                        propertyID = propertyvalue.PropertyID 
                    });
                }
               else
                {
                    WidegtProperites[propertyid].Values.Add(new PropertyValueDTO
                    {
                        Id = propertyvalue.Id,
                        isDefault = propertyvalue.IsDefault,
                        value = propertyvalue.Value,
                        propertyID = propertyvalue.PropertyID
                    });
                }

            }
            Console.WriteLine("Start Coping Units to each property");
            var PropertiesWithUnits = await (from p in _context.Properties
                                              join wp in _context.WidgetProperties
                                              on p.Id equals wp.Property.Id
                                              join pu in _context.PropertyUnits
                                              on p.Id equals pu.PropertyID
                                              join u in _context.Units
                                              on pu.UnitID equals u.Id
                                              where wp.WidgetID == id
                                              orderby p.Id ascending
                                              select new
                                              {
                                                 name = u.UnitName,
                                                 DefaultUnitOfProperty = pu.isDefault,
                                                 DefaultOfUnit = u.isDefault,
                                                 unitId = u.Id,
                                                 PropertyID = pu.PropertyID
                                              }
                                  ).ToListAsync();

            foreach (var propertyunit in PropertiesWithUnits)
            {
                int propertyid = propertyunit.PropertyID;
                if (WidegtProperites[propertyid].Units == null)
                {
                    WidegtProperites[propertyid].Units = new List<PropertyUnitDTO>();
                    WidegtProperites[propertyid].Units.Add(new PropertyUnitDTO
                    {
                        UnitName = propertyunit.name,
                        UnitId = propertyunit.PropertyID,
                        PropertyID = propertyunit.PropertyID,
                        DefaultOfUnit = propertyunit.DefaultOfUnit,
                        DefaultUnitOfProperty = propertyunit.DefaultUnitOfProperty
                    });
                }
                else
                {
                    WidegtProperites[propertyid].Units.Add(new PropertyUnitDTO
                    {
                        UnitName = propertyunit.name,
                        UnitId = propertyunit.PropertyID,
                        PropertyID = propertyunit.PropertyID,
                        DefaultOfUnit = propertyunit.DefaultOfUnit,
                        DefaultUnitOfProperty = propertyunit.DefaultUnitOfProperty
                    });
                }

            }
            return WidegtProperites.Values.ToList();
        }
    }
}
// TODO: re - structure viewmodel class to both property value and property unit