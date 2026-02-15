import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Trip, TripsApiResponse } from '../models/trips.models';
import { map } from 'rxjs/internal/operators/map';
import { API_URL } from '../../../shared/constants';
import { TripItinerary } from '../models/tripItinerary.models';
import { Destination } from '../models/destinations.models';

@Injectable({
    providedIn: 'root',
})
export class TripsService {
    private readonly http = inject(HttpClient);

    getPaginatedTrips(
        skip: number,
        take: number,
        sortField: string = 'City',
        sortDirection: string = 'Ascending',
    ) {
        const params = {
            skip,
            take,
            sortField,
            sortDirection,
        };

        return this.http.get<TripsApiResponse>(`${API_URL}/api/trips`, { params }).pipe(
            map((response) => ({
                ...response,
                items: response.items.map((a) => ({
                    ...a,
                    startAt: new Date(a.startAt as unknown as string),
                    endAt: new Date(a.endAt as unknown as string),
                })),
            })),
        );
    }

    createTrip(trip: Trip) {
        return this.http.post<Trip>(`${API_URL}/api/trips`, trip);
    }

    updateTrip(id: string, trip: Trip) {
        return this.http.put<Trip>(`${API_URL}/api/trips/${id}`, trip);
    }

    getTripById(id: string) {
        return this.http.get<Trip>(`${API_URL}/api/trips/${id}`);
    }

    deleteTrip(id: string) {
        return this.http.delete(`${API_URL}/api/trips/${id}`);
    }

    getTripItineraries(id: string) {
        return this.http.get<TripItinerary[]>(`${API_URL}/api/trips/${id}/itinerary`);
    }

    getDestinations(search: string) {
        return this.http
            .get<Destination[]>(`${API_URL}/api/destinations?search=${search}`);
    }
}
