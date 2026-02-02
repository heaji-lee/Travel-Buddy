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

export interface CountryMap {
    [code: string]: string;
}

export interface City {
    name: string;
    country: string;
}

export interface Destination {
  city: string;
  country: string;
}