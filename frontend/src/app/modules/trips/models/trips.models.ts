import { Companion, Interest, TravelStyle } from "../../manage/models/manage.models";
import { TripItinerary } from "./tripItinerary.models";
export interface Trip {
    id?: string;
    name: string;
    city: string;
    country: string;
    startAt: Date;
    endAt: Date;
    companions?: Companion[];
    interests?: Interest[];
    travelStyles?: TravelStyle[];
    tripItineraries?: TripItinerary[];
}

export interface TripsApiResponse {
    items: Trip[];
    total: number;
}