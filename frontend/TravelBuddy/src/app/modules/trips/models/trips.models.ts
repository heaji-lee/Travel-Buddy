export interface Trip {
    id: string;
    name: string;
    city: string;
    country: string;
    startAt: Date;
    endAt: Date;
}

export interface TripsApiResponse {
    items: Trip[];
    total: number;
}