// import { Component, computed, inject, signal } from '@angular/core';
// import { RouterLink } from '@angular/router';
// import { rxResource } from '@angular/core/rxjs-interop';
// import { CommonModule } from '@angular/common';

// import { HeaderComponent } from '../../../../../../../../packages/ui/projects/ui/src/lib/components/header/header.component';
// import { AssigneesService } from '../../services/assignees.service';
// import { PaginationComponent } from '../../../../../../../../packages/ui/projects/ui/src/lib/components/pagination/pagination.component';
// import { Assignee } from '../../models/assignees.models';
// import { PAGE_SIZE } from '../../../../core/tokens';

// import { DialogModule } from 'primeng/dialog';
// import { ButtonModule } from 'primeng/button';
// import { TableModule } from 'primeng/table';
// import { IconFieldModule } from 'primeng/iconfield';
// import { InputIconModule } from 'primeng/inputicon';
// import { InputTextModule } from 'primeng/inputtext';
// import { MessageService } from 'primeng/api';
// import { ReviewChangesDialogComponent } from '../../../../components/review-changes-dialog/review-changes-dialog.component';

// @Component({
//     selector: 'app-assignees-list',
//     imports: [
//         HeaderComponent,
//         ButtonModule,
//         PaginationComponent,
//         DialogModule,
//         TableModule,
//         RouterLink,
//         IconFieldModule,
//         InputIconModule,
//         CommonModule,
//         InputTextModule,
//         ReviewChangesDialogComponent
//     ],
//     templateUrl: './assignees-list.component.html',
//     styleUrls: ['./assignees-list.component.css']
// })
// export class AssigneesListComponent {
//     private readonly assigneesService = inject(AssigneesService);
//     private readonly messageService = inject(MessageService);
//     readonly pageSize = inject(PAGE_SIZE);

//     page = signal(1);
//     searchTerm = signal('');
//     EXCLUDE_ASSIGNED = false;
//     IS_ON_WHITELIST = undefined;
//     sortField = signal<'FullName' | 'ExpiryDate'>('FullName');
//     sortDirection = signal<'Ascending' | 'Descending'>('Ascending');
//     assigneeId: string = '';
//     isDeleteDialogOpened = false;
//     selectedAssignee = signal<Assignee | null>(null);

//     // Review Changes Dialog State
//     reviewChangesDialogIsVisible = signal(false);
//     assigneeUpdates = signal<Record<string, Partial<Assignee>>>({});

//     assignees = rxResource({
//         request: () => ({
//             skip: this.skip(),
//             searchTerm: this.searchTerm(),
//             sortField: this.sortField(),
//             sortDirection: this.sortDirection()
//         }),
//         loader: ({ request }) =>
//             this.assigneesService.getPaginatedAssignees(
//                 request.skip,
//                 this.pageSize,
//                 request.searchTerm,
//                 this.EXCLUDE_ASSIGNED,
//                 this.IS_ON_WHITELIST,
//                 request.sortField,
//                 request.sortDirection
//             )
//     });

//     totalRecords = computed(() => this.assignees.value()?.total ?? 0);
//     skip = computed(() => (this.page() - 1) * this.pageSize);

//     handleSearch(event: Event) {
//         this.searchTerm.set((event.target as HTMLInputElement).value);
//         this.page.set(1);
//     }

//     goToNextPage() {
//         const maxPage = Math.ceil(this.totalRecords() / this.pageSize);
//         this.page.update((p) => (p < maxPage ? p + 1 : p));
//     }

//     goToPreviousPage() {
//         this.page.update((p) => (p > 1 ? p - 1 : p));
//     }

//     openDeleteDialog(assignee: Assignee) {
//         const isAssigneeOnTheWhitelist =
//             assignee.isOnWhitelist && assignee.whitelistStatus === 'Allowed';
//         this.selectedAssignee.set(assignee);

//         // Open the review changes dialog if the assignee is on the whitelist
//         if (isAssigneeOnTheWhitelist) {
//             this.assigneeUpdates.set({
//                 [assignee.id]: { ...assignee, isOnWhitelist: false }
//             });
//             this.reviewChangesDialogIsVisible.set(true);
//             return;
//         }

//         // Otherwise proceed to open the delete dialog without whitelist update
//         this.isDeleteDialogOpened = true;
//     }

//     handleConfirmDelete(assigneeId: string) {
//         this.assigneesService.deleteAssignee(assigneeId).subscribe({
//             next: () => {
//                 this.messageService.add({
//                     key: 'globalToast',
//                     severity: 'success',
//                     summary: `Assignee deleted`,
//                     detail: `${this.selectedAssignee()?.fullName} has been deleted`,
//                     life: 3000,
//                     styleClass: 'border-surface-200'
//                 });
//                 this.assignees.reload();
//                 this.isDeleteDialogOpened = false;
//                 this.reviewChangesDialogIsVisible.set(false);
//                 this.selectedAssignee.set(null);
//             },
//             error: (error) => {
//                 this.messageService.add({
//                     key: 'globalToast',
//                     severity: 'error',
//                     summary: 'Something went wrong',
//                     detail: error,
//                     life: 3000,
//                     styleClass: 'border-surface-200'
//                 });
//             }
//         });
//     }

//     sortBy(event: { field: string; order: number }) {
//         this.sortField.set(event.field === 'fullName' ? 'FullName' : 'ExpiryDate');
//         this.sortDirection.set(event.order === 1 ? 'Ascending' : 'Descending');
//     }
// }
