import { Component, input } from '@angular/core';

@Component({
  selector: 'student-table',
  standalone: false,
  template: ` <div class="table-responsive-xl">
    <table class="table table-striped">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th class="single-line" scope="col">First Name</th>
          <th class="single-line" scope="col">Last Name</th>
          <th class="single-line" scope="col">Email</th>
          <th class="single-line" scope="col">Degree</th>
          <th class="single-line" scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        @for (student of students(); track student.id; let ind = $index) {
          <tr
            student-table-row
            [firstName]="student.firstName"
            [lastName]="student.lastName"
            [email]="student.email"
            [degreeTitle]="student.degreeTitle"
            [id]="student.id"
            [index]="getIndex(ind)"
          ></tr>
        }
      </tbody>
    </table>
  </div>`,
})
export class StudentTableComponent {
  students = input.required<any[]>();
  currentPage = input.required<number>();
  pageSize = input.required<number>();
  getIndex(ind: number) {
    const page = this.currentPage();
    const pageSize = this.pageSize();
    const result = (page - 1) * pageSize + ind + 1;
    return result;
  }
}
