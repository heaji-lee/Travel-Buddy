export interface Trip {
    id: string;
    city: string;
    country: string;
    startAt: Date;
    endAt: Date;
}

export interface TripsApiResponse {
    items: Trip[];
    total: number;
}