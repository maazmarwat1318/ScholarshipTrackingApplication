import { Component, input, model } from '@angular/core';

@Component({
  selector: 'moderator-table',
  standalone: false,
  template: ` <div class="table-responsive-xl">
    <table class="table table-striped">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th class="single-line" scope="col">First Name</th>
          <th class="single-line" scope="col">Last Name</th>
          <th class="single-line" scope="col">Email</th>
          <th class="single-line" scope="col">Role</th>
          <th class="single-line" scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        @for (moderator of moderators(); track moderator.id; let ind = $index) {
          <tr
            (moderatorDeleted)="onModeratorDeleted(ind)"
            moderator-table-row
            [firstName]="moderator.firstName"
            [lastName]="moderator.lastName"
            [email]="moderator.email"
            [role]="moderator.role"
            [id]="moderator.id"
            [index]="getIndex(ind)"
          ></tr>
        }
      </tbody>
    </table>
  </div>`,
})
export class ModeratorTableComponent {
  moderators = model.required<any[]>();
  currentPage = input.required<number>();
  pageSize = input.required<number>();
  getIndex(ind: number) {
    const page = this.currentPage();
    const pageSize = this.pageSize();
    const result = (page - 1) * pageSize + ind + 1;
    return result;
  }

  onModeratorDeleted(index: number) {
    this.moderators().splice(index, 1);
  }
}
