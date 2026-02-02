export interface Companion {
    id?: string;
    name: string;
}

export interface Interest {
    id?: string;
    interest: string;
}

export interface TravelStyle {
  id?: string;
  style: string;
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