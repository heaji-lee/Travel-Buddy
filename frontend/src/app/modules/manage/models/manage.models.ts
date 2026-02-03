export interface Companion {
    id?: string;
    name: string;
}

export interface Interest {
    id?: string;
    name: string;
}

export interface TravelStyle {
  id?: string;
  name: string;
}

export interface CompanionsApiResponse {
    items: Companion[];
    total: number;
}

export interface InterestsApiResponse {
    items: Interest[];
    total: number;
}

export interface TravelStylesApiResponse {
  items: TravelStyle[];
  total: number;
}