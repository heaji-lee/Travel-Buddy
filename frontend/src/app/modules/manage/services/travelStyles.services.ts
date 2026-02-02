import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { TravelStyle, TravelStylesApiResponse } from '../models/manage.models';
import { map } from 'rxjs/internal/operators/map';
import { API_URL } from '../../../shared/constants';

@Injectable({
    providedIn: 'root',
})
export class TravelStylesService {
    private readonly http = inject(HttpClient);

    getPaginatedTravelStyles(skip: number, take: number) {
        const params = {
            skip,
            take,
        };

        return this.http
            .get<TravelStylesApiResponse>(`${API_URL}/api/travel-styles`, { params })
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
        return this.http.post<TravelStyle>(`${API_URL}/api/travel-styles`, travelStyle);
    }

    deleteTravelStyle(id: string) {
        return this.http.delete(`${API_URL}/api/travel-styles/${id}`);
    }
}
