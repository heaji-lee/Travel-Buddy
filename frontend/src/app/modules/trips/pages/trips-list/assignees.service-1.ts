// import { HttpClient } from '@angular/common/http';
// import { inject, Injectable } from '@angular/core';

// import { Assignee, AssigneeRolesApiResponse, AssigneesApiResponse } from '../models/assignees.models';
// import { map } from 'rxjs/internal/operators/map';
// import { API_URL } from '../../../core/tokens';

// @Injectable({
//     providedIn: 'root'
// })
// export class AssigneesService {
//     private readonly http = inject(HttpClient);
//     private readonly apiUrl = inject(API_URL);

//     getPaginatedAssignees(
//         skip: number,
//         take: number,
//         searchTerm: string,
//         excludeAssigned: boolean = false,
//         isOnWhitelist?: boolean,
//         sortField: string = 'FullName',
//         sortDirection: string = 'Ascending'
//     ) {
//         const params = {
//             skip,
//             take,
//             searchTerm,
//             excludeAssigned,
//             ...(isOnWhitelist !== undefined && { isOnWhitelist }),
//             sortField,
//             sortDirection
//         };

//         return this.http.get<AssigneesApiResponse>(`${this.apiUrl}/api/assignees`, { params }).pipe(
//             map((response) => ({
//                 ...response,
//                 items: response.items.map(a => ({
//                     ...a,
//                     expiryDate: new Date(a.expiryDate as unknown as string)
//                 }))
//             }))
//         );
//     }

//     getAssigneeById(id: string) {
//         return this.http.get<Assignee>(`${this.apiUrl}/api/assignees/${id}`).pipe(
//             map((response) => {
//                 response.expiryDate = new Date(response.expiryDate as unknown as string);
//                 return response;
//             })
//         );
//     }

//     getAssigneeRoles() {
//         return this.http.get<AssigneeRolesApiResponse>(`${this.apiUrl}/api/assignees/roles`);
//     }

//     getAssigneesById(ids: string) {
//         return this.http
//             .get<{ items: Assignee[]; total: number }>(`${this.apiUrl}/api/assignees/by-ids?ids=${ids}`)
//             .pipe(
//                 map((response) => {
//                     response.items.map((a) => {
//                         a.expiryDate = new Date(a.expiryDate as unknown as string);
//                     });
//                     return response;
//                 })
//             );
//     }

//     getWhitelistedAssigneesCount(isAllowed: boolean) {
//         return this.http.get<{ count: number }>(
//             `${this.apiUrl}/api/assignees/whitelisted?isAllowed=${isAllowed}`
//         );
//     }

//     createAssignee(assignee: Assignee) {
//         return this.http.post<Assignee>(`${this.apiUrl}/api/assignees`, assignee);
//     }

//     updateAssignee(id: string, assignee: Assignee) {
//         return this.http.put<Assignee>(`${this.apiUrl}/api/assignees/${id}`, assignee);
//     }

//     updateAssignees(assignees: Partial<Assignee>[]) {
//         return this.http.patch<{modifiedCount: number}>(`${this.apiUrl}/api/assignees/batch`, assignees);
//     }

//     deleteAssignee(id: string) {
//         return this.http.delete(`${this.apiUrl}/api/assignees/${id}`);
//     }
// }
