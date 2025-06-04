import { NgModule } from '@angular/core';
import { Route, Router, RouterModule } from '@angular/router';
import { ModeratorLayoutComponent } from './moderator-layout.component';
import { ManageStudentsComponent } from './StudentManagement/manage-students.component';
import { CreateStudentComponent } from './StudentManagement/Components/create-student.component';
import { ViewStudentsComponent } from './StudentManagement/Components/view-students.component';
import { EditStudentComponent } from './StudentManagement/Components/edit-student.component';

const routes: Route[] = [
  {
    path: '',
    component: ModeratorLayoutComponent,
    children: [
      {
        path: 'students',
        component: ManageStudentsComponent,
        children: [
          {
            path: '',
            pathMatch: 'full',
            redirectTo: 'view',
          },
          {
            path: 'view',
            pathMatch: 'full',
            component: ViewStudentsComponent,
          },
          {
            path: 'create',
            component: CreateStudentComponent,
          },
          {
            path: 'edit/:id',
            component: EditStudentComponent,
          },
        ],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModeratorSectionRoutingModule {}
