// import {  Injectable } from '@angular/core';
// import { City, CountryMap, Destination } from '../models/trips.models';
// import cities from '../../../../assets/data/cities.json';
// import countries from '../../../../assets/data/countries.json';

// @Injectable({
//     providedIn: 'root',
// })
// export class DestinationService {
//     private cities = cities as City[];
//     private countries = countries as CountryMap;

//     searchCities(query: string): Destination[] {
//       if (!query || query.length < 2) return [];

//       const q = query.toLowerCase();

//       return this.cities
//         .filter(c => c.name.toLowerCase().includes(q))
//         .slice(0, 10)
//         .map(c => ({ 
//           city: c.name, 
//           country: this.countries[c.country] ?? 'Unknown'
//         }));
//     }

// }