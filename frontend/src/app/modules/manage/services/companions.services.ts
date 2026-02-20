import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Companion, CompanionsApiResponse } from '../models/manage.models';
import { map } from 'rxjs/internal/operators/map';
import { API_URL } from '../../../shared/constants';

@Injectable({
    providedIn: 'root',
})
export class CompanionsService {
    private readonly http = inject(HttpClient);

    getPaginatedCompanions(skip: number, take: number) {
        const params = {
            skip,
            take,
        };

        return this.http.get<CompanionsApiResponse>(`${API_URL}/api/companions`, { params });
    }

    createCompanion(companion: Companion) {
        return this.http.post<Companion>(`${API_URL}/api/companions`, companion);
    }

    deleteCompanion(id: string) {
        return this.http.delete(`${API_URL}/api/companions/${id}`);
    }
}
