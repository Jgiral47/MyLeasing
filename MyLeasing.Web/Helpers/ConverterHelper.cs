﻿using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entity;
using MyLeasing.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {

        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(
            DataContext DataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = DataContext;
            _combosHelper = combosHelper;
        }

        public async Task<Property> ToPropertyAsync(PropertyViewModel model, bool isNew)
        {
            return new Property
            {
                Address = model.Address,
                Contracts = isNew ? new List<Contract>() : model.Contracts,
                HasParkingLot = model.HasParkingLot,
                Id = isNew ? 0 : model.Id,
                IsAvailable = model.IsAvailable,
                Neighborhood = model.Neighborhood,
                Owner = await _dataContext.Owners.FindAsync(model.OwnerId),
                Price = model.Price,
                PropertyImages = isNew ? new List<PropertyImage>() : model.PropertyImages,
                PropertyType = await _dataContext.PropertyTypes.FindAsync(model.PropertyTypeId),
                Remarks = model.Remarks,
                Rooms = model.Rooms,
                SquareMeters = model.SquareMeters,
                Stratum = model.Stratum
            };
        }

        public PropertyViewModel ToPropertyViewModel(Property property)
        {
            return new PropertyViewModel
            {
                Address = property.Address,
                HasParkingLot = property.HasParkingLot,
                Id = property.Id,
                IsAvailable = property.IsAvailable,
                Neighborhood = property.Neighborhood,
                Price = property.Price,
                Rooms = property.Rooms,
                SquareMeters = property.SquareMeters,
                Stratum = property.Stratum,
                Owner = property.Owner,
                OwnerId = property.Owner.Id,
                PropertyType = property.PropertyType,
                PropertyTypeId = property.PropertyType.Id,
                PropertyTypes = _combosHelper.GetComboPropertyTypes(),
                Remarks = property.Remarks,


            };
        }



        public async Task<Contract> ToContractAsync(ContractViewModel model, bool isNew)
        {
            return new Contract
            {
                Id = isNew ? 0 : model.Id,
                EndDate = model.EndDate.ToUniversalTime(),
                IsActive = model.IsActive,
                Lessee = await _dataContext.Lessees.FindAsync(model.LesseeId),
                Owner = await _dataContext.Owners.FindAsync(model.OwnerId),
                Price = model.Price,
                Property = await _dataContext.Properties.FindAsync(model.PropertyId),
                Remarks = model.Remarks,
                StartDate = model.StartDate.ToUniversalTime()
            };
        }

        public ContractViewModel ToContractViewModel(Contract contract)
        {
            return new ContractViewModel
            {
                Id = contract.Id,
                EndDate = contract.EndDateLocal,
                IsActive = contract.IsActive,
                Lessee = contract.Lessee,
                Owner = contract.Owner,
                Price = contract.Price,
                Property = contract.Property,
                Remarks = contract.Remarks,
                StartDate = contract.StartDateLocal,
                LesseeId = contract.Lessee.Id,
                Lessees = _combosHelper.GetComboLessees(),
                OwnerId = contract.Property.Id,
                PropertyId = contract.Property.Id
            };
        }
    }
}
