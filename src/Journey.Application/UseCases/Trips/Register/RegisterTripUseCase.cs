﻿using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.Register;
public class RegisterTripUseCase
{
    public ResponseShortTripJson Execute(RequestRegisterTripJson request)
    {
        Validate(request);

        var dbContext = new JourneyDbContext();

        var entity = new Trip
        {
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };

        dbContext.Trips.Add(entity);

        dbContext.SaveChanges();

        return new ResponseShortTripJson 
        {
            EndDate = entity.EndDate,
            StartDate = entity.StartDate,
            Name = entity.Name,
            Id = entity.Id
        };
    }

    private void Validate(RequestRegisterTripJson request)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            throw new JourneyException(ResourceErroMessages.NAME_EMPTY);
        }

        if(request.StartDate < DateTime.UtcNow.Date)
        {
            throw new JourneyException(ResourceErroMessages.DATA_TRIP_MUST_BE_LATER_THAN_TODAY);
        }

        if (request.EndDate < request.StartDate.Date)
        {
            throw new JourneyException(ResourceErroMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE);
        }
    }
}
