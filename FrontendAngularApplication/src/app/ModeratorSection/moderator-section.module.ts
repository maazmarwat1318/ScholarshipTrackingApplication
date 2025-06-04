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
import { ManageModeratorsComponent } from './ModeratorManagement/manage-moderators.component';
import { ViewModeratorsComponent } from './ModeratorManagement/Components/view-moderators.component';
import { ModeratorTableRow } from './ModeratorManagement/Components/moderator-table-row.component';
import { ModeratorSearchFieldComponent } from './ModeratorManagement/Components/moderator-search-field.component';
import { ModeratorTableComponent } from './ModeratorManagement/Components/moderator-table.component';
import { CreateModeratorComponent } from './ModeratorManagement/Components/create-moderator.component';
import { EditModeratorComponent } from './ModeratorManagement/Components/edit-moderator.component';

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
    ManageModeratorsComponent,
    ViewModeratorsComponent,
    ModeratorTableRow,
    ModeratorSearchFieldComponent,
    ModeratorTableComponent,
    CreateModeratorComponent,
    EditModeratorComponent,
  ],
  imports: [CommonModule, ModeratorSectionRoutingModule, ReactiveFormsModule],
  providers: [TitleService, ActionDialogService, ContentDialogService],
})
export class ModeratorSectionModule {}
