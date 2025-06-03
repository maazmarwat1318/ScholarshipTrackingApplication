import { NgModule } from '@angular/core';
import { Route, Router, RouterModule } from '@angular/router';
import { ModeratorLayoutComponent } from './moderator-layout.component';
import { ManageStudentsComponent } from './StudentManagement/manage-students.component';

const routes: Route[] = [
  {
    path: '',
    component: ModeratorLayoutComponent,
    children: [
      {
        path: 'students',
        component: ManageStudentsComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModeratorSectionRoutingModule {}
