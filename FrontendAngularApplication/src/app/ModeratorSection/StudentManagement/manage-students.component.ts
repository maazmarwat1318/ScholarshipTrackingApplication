import {
  Component,
  computed,
  effect,
  input,
  OnDestroy,
  OnInit,
  signal,
  TemplateRef,
} from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TitleService } from '../Service/TitleService';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../../Services/UserService';
import { ActivatedRoute, Router } from '@angular/router';
import { convertWithSafeFallBackToValue } from '../../Common/Helpers/NumberHelper';
import { ActionDialogService } from '../../Common/Alerts/ActionDialog/ActionDialogService';
import { SnackbarService } from '../../Common/Alerts/Snackbar/SnackbarService';
import { ContentDialogService } from '../../Common/Alerts/ContentDialog/ContentDialogService';

@Component({
  selector: 'manage-students',
  template: ` <router-outlet></router-outlet> `,
  standalone: false,
})
export class ManageStudentsComponent {}
