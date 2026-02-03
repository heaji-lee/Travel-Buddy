import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Interest, InterestsApiResponse } from '../models/manage.models';
import { map } from 'rxjs/internal/operators/map';
import { API_URL } from '../../../shared/constants';

@Injectable({
    providedIn: 'root',
})
export class InterestsService {
    private readonly http = inject(HttpClient);

    getPaginatedInterests(skip: number, take: number) {
        const params = {
            skip,
            take,
        };

        return this.http.get<InterestsApiResponse>(`${API_URL}/api/interests`, { params });
    }

    createInterest(interest: Interest) {
        return this.http.post<Interest>(`${API_URL}/api/interests`, interest);
    }

    deleteInterest(id: string) {
        return this.http.delete(`${API_URL}/api/interests/${id}`);
    }
}
