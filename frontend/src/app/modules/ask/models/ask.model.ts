export interface GenerateTripRequest {
  city: string; 
  days: number;
  preferences: string;
}

export interface GenerateTripResponse {
  itinerary: string[];
}