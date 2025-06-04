import { NgModule } from '@angular/core';
import { CommonModule } from '../Common/common.module';
import { ModeratorLayoutComponent } from './moderator-layout.component';
import { ModeratorSectionRoutingModule } from './moderator-section-routes.module';
import { ModeratorSideNav } from './SideNavBar/moderator-side-nav.component';
import { ModeratorSideNavTile } from './SideNavBar/moderator-side-nav-tile.component';
import { ManageStudentsComponent } from './StudentManagement/manage-students.component';
import { TitleService } from './Service/TitleService';
import { StudentTableRow } from './StudentManagement/Components/student-table-row.component';
import { StudentTableComponent } from './StudentManagement/Components/student-table.component';
import { StudentSearchFieldComponent } from './StudentManagement/Components/student-search-field.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ActionDialogService } from '../Common/Alerts/ActionDialog/ActionDialogService';
import { ContentDialogService } from '../Common/Alerts/ContentDialog/ContentDialogService';
import { CreateStudentComponent } from './StudentManagement/Components/create-student.component';
import { ViewStudentsComponent } from './StudentManagement/Components/view-students.component';
import { EditStudentComponent } from './StudentManagement/Components/edit-student.component';

@NgModule({
  declarations: [
    ModeratorLayoutComponent,
    ModeratorSideNav,
    ModeratorSideNavTile,
    ManageStudentsComponent,
    StudentTableRow,
    StudentTableComponent,
    StudentSearchFieldComponent,
    CreateStudentComponent,
    ViewStudentsComponent,
    EditStudentComponent,
  ],
  imports: [CommonModule, ModeratorSectionRoutingModule, ReactiveFormsModule],
  providers: [TitleService, ActionDialogService, ContentDialogService],
})
export class ModeratorSectionModule {}
