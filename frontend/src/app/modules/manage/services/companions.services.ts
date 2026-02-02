import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import {
    Companion,
    Interest,
    TravelStyle,
    CompanionsApiResponse,
    InterestsApiResponse,
    TravelStylesApiResponse,
} from '../models/manage.models';
import { map } from 'rxjs/internal/operators/map';
import { API_URL } from '../../../shared/constants';

@Injectable({
    providedIn: 'root',
})
export class ManageService {
    private readonly http = inject(HttpClient);

    getPaginatedCompanions(skip: number, take: number) {
        const params = {
            skip,
            take,
        };

        return this.http
            .get<CompanionsApiResponse>(`${API_URL}/api/manage/companions`, { params })
            .pipe(
                map((response) => ({
                    ...response,
                    items: response.items.map((a) => ({
                        ...a,
                    })),
                })),
            );
    }

    createTrip(companion: Companion) {
        return this.http.post<Companion>(`${API_URL}/api/manage/companions`, companion);
    }

    deleteTrip(id: string) {
        return this.http.delete(`${API_URL}/api/manage/companions/${id}`);
    }

    getPaginatedInterests(skip: number, take: number) {
        const params = {
            skip,
            take,
        };

        return this.http
            .get<InterestsApiResponse>(`${API_URL}/api/manage/interests`, { params })
            .pipe(
                map((response) => ({
                    ...response,
                    items: response.items.map((a) => ({
                        ...a,
                    })),
                })),
            );
    }

    createInterest(interest: Interest) {
        return this.http.post<Interest>(`${API_URL}/api/manage/interests`, interest);
    }

    deleteInterest(id: string) {
        return this.http.delete(`${API_URL}/api/manage/interests/${id}`);
    }

    getPaginatedTravelStyles(skip: number, take: number) {
        const params = {
            skip,
            take,
        };

        return this.http
            .get<TravelStylesApiResponse>(`${API_URL}/api/manage/travel-styles`, { params })
            .pipe(
                map((response) => ({
                    ...response,
                    items: response.items.map((a) => ({
                        ...a,
                    })),
                })),
            );
    }

    createTravelStyle(travelStyle: TravelStyle) {
        return this.http.post<TravelStyle>(`${API_URL}/api/manage/travel-styles`, travelStyle);
    }

    deleteTravelStyle(id: string) {
        return this.http.delete(`${API_URL}/api/manage/travel-styles/${id}`);
    }
}
