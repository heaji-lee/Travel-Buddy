import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { API_URL } from '../../../shared/constants';
import { GenerateTripRequest, GenerateTripResponse } from '../models/ask.model';

@Injectable({
    providedIn: 'root',
})
export class AskService {
    private readonly http = inject(HttpClient);

    generateItinerary(request: GenerateTripRequest) {
        return this.http.post<GenerateTripResponse>(`${API_URL}/api/ask/generate`, request);
    }
}
