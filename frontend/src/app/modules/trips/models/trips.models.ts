import { Companion, Interest, TravelStyle } from "../../manage/models/manage.models";

export interface Trip {
    id?: string;
    name: string;
    city: string;
    startAt: Date;
    endAt: Date;
    companions?: Companion[];
    interests?: Interest[];
    travelStyles?: TravelStyle[];
}

export interface TripsApiResponse {
    items: Trip[];
    total: number;
}