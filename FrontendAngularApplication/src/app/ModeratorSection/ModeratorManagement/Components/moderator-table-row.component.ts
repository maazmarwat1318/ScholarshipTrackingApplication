import { Component, input, output } from '@angular/core';
import { ActionDialogService } from '../../../Common/Alerts/ActionDialog/ActionDialogService';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../../../Services/UserService';
import { SnackbarService } from '../../../Common/Alerts/Snackbar/SnackbarService';

@Component({
  selector: '[moderator-table-row]',
  template: ` <th scope="row">{{ index() }}</th>
    <td>{{ firstName() }}</td>
    <td>{{ lastName() }}</td>
    <td>{{ email() }}</td>
    <td>{{ role() }}</td>
    <td>
      <div class="d-flex gap-2 align-items-center">
        <a
          [routerLink]="['../edit', id()]"
          class="btn btn-primary btn-sm d-flex flex-nowrap align-items-center"
        >
          <i class="bi bi-pencil-square me-1"></i> <span>Edit</span>
        </a>

        <button
          (click)="onDeleteClicked()"
          class="btn btn-primary btn-sm card-delete-button btn-danger d-flex flex-nowrap align-items-center"
        >
          <i class="bi bi-trash me-1"></i> <span>Delete</span>
        </button>
      </div>
    </td>`,
  standalone: false,
})
export class ModeratorTableRow {
  firstName = input.required<string>();
  lastName = input.required<string>();
  id = input.required<number>();
  email = input.required<string>();
  role = input.required<string>();
  index = input.required<number>();
  moderatorDeleted = output<void>();
  constructor(
    public actionDialogService: ActionDialogService,
    public _httpClient: HttpClient,
    public _userService: UserService,
    public _snackbarService: SnackbarService,
  ) {}

  onDeleteClicked() {
    this.actionDialogService.showConfirmationDialog(
      'Delete User',
      'Are you sure you want to delete this user?',
      'Delete',
      'danger',
      () => {
        this.deleteModerator(this.id());
      },
    );
  }

  deleteModerator(id: number) {
    this.actionDialogService.isLoading.set(true);
    this._httpClient
      .delete(`https://localhost:7222/ScholarshipModerator/${id}`, {
        headers: { Authorization: `Bearer ${this._userService.authToken!}` },
      })
      .subscribe(
        (response) => {
          this.moderatorDeleted.emit();
          this.actionDialogService.hideDialog();
        },
        (error) => {
          this._snackbarService.showErrorSnackbar(
            error.error?.message ?? 'Unknown error occured',
          );
          this.actionDialogService.isLoading.set(false);
        },
        () => {
          this.actionDialogService.isLoading.set(false);
        },
      );
  }
}
