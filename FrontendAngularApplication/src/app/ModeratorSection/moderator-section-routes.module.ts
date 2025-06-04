import { NgModule } from '@angular/core';
import { Route, Router, RouterModule } from '@angular/router';
import { ModeratorLayoutComponent } from './moderator-layout.component';
import { ManageStudentsComponent } from './StudentManagement/manage-students.component';
import { CreateStudentComponent } from './StudentManagement/Components/create-student.component';
import { ViewStudentsComponent } from './StudentManagement/Components/view-students.component';
import { EditStudentComponent } from './StudentManagement/Components/edit-student.component';
import { ManageModeratorsComponent } from './ModeratorManagement/manage-moderators.component';
import { ViewModeratorsComponent } from './ModeratorManagement/Components/view-moderators.component';
import { CreateModeratorComponent } from './ModeratorManagement/Components/create-moderator.component';
import { EditModeratorComponent } from './ModeratorManagement/Components/edit-moderator.component';

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
      {
        path: 'moderators',
        component: ManageModeratorsComponent,
        children: [
          {
            path: '',
            pathMatch: 'full',
            redirectTo: 'view',
          },
          {
            path: 'view',
            pathMatch: 'full',
            component: ViewModeratorsComponent,
          },
          {
            path: 'create',
            component: CreateModeratorComponent,
          },
          {
            path: 'edit/:id',
            component: EditModeratorComponent,
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
