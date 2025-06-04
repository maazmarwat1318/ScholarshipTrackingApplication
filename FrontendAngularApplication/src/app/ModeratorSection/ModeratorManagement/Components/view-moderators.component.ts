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
import { TitleService } from '../../Service/TitleService';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../../../Services/UserService';
import { ActivatedRoute, Router } from '@angular/router';
import { convertWithSafeFallBackToValue } from '../../../Common/Helpers/NumberHelper';
import { ActionDialogService } from '../../../Common/Alerts/ActionDialog/ActionDialogService';
import { SnackbarService } from '../../../Common/Alerts/Snackbar/SnackbarService';
import { ContentDialogService } from '../../../Common/Alerts/ContentDialog/ContentDialogService';

@Component({
  selector: 'view-moderators',
  template: `
    @if (isFetchLoading() && page() === 1 && moderators().length === 0) {
      <div
        class="min-full-height-inside-main-layout d-flex align-items-center justify-content-center"
      >
        <div
          class="spinner-border text-success"
          style="width: 50px; height: 50px;"
        ></div>
      </div>
    } @else if (
      error() !== undefined && page() === 1 && moderators().length === 0
    ) {
      <div
        class="alert alert-danger mx-auto text-center"
        style="max-width: 400px;"
      >
        <p>{{ error() }}</p>
        <button
          class="btn btn-primary"
          (click)="this.getStudents(page(), searchString())"
        >
          Retry
        </button>
      </div>
    } @else {
      <div class="d-flex justify-content-end align-items-center mb-3 gap-2">
        <moderator-search-field
          [searchParam]="searchString()"
          (searchClicked)="this.goToPage(1, $event)"
        />
        <button class="btn btn-success" routerLink="../create">
          + Create Moderator
        </button>
      </div>
      @if (moderators().length === 0) {
        <p class="text-center text-muted">No moderators found</p>
      } @else {
        <moderator-table
          [(moderators)]="moderators"
          [pageSize]="pageSize"
          [currentPage]="page()"
        ></moderator-table>
      }
      <div class="d-flex justify-content-center gap-2">
        <button
          class="btn btn-link text-decoration-none"
          [disabled]="isFirstPage()"
          (click)="this.goToPage(page() - 1, searchString())"
        >
          <i class="bi-chevron-left"></i> Previous
        </button>
        <button
          class="btn btn-link text-decoration-none"
          [disabled]="isLastPage()"
          (click)="this.goToPage(page() + 1, searchString())"
        >
          Next <i class="bi-chevron-right"></i>
        </button>
      </div>
    }
  `,
  standalone: false,
})
export class ViewModeratorsComponent implements OnDestroy {
  moderators = signal<any[]>([]);
  page = signal(1);
  searchString = signal('');
  pageSize = 10;
  isLastPage = signal(false);
  isFirstPage = computed(() => this.page() === 1);
  isFetchLoading = signal(false);
  error = signal<any>(undefined);
  constructor(
    private _titleService: TitleService,
    private _httpClient: HttpClient,
    private _dialogService: ActionDialogService,
    private _snackbarService: SnackbarService,
    private _userService: UserService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
  ) {
    _titleService.setTitle('View Moderators');
    _activatedRoute.queryParams.subscribe((params) => {
      _snackbarService.hideSnackbar();
      _dialogService.hideDialog();
      let page = convertWithSafeFallBackToValue(params['page']);
      let searchString = params['searchString'] ?? '';
      this.searchString.set(searchString);
      this.getStudents(page, searchString);
    });
  }
  ngOnDestroy(): void {
    this._snackbarService.hideSnackbar();
    this._dialogService.hideDialog();
  }

  getStudents(page: number, searchString: string = '') {
    this.isFetchLoading.set(true);
    this.error.set(undefined);
    this._httpClient
      .get('https://localhost:7222/ScholarshipModerator', {
        params: {
          page: page,
          searchString: searchString,
        },
        headers: { Authorization: `Bearer ${this._userService.authToken!}` },
      })
      .subscribe(
        (response) => {
          this.moderators.set((response as any).moderators);
          this.isLastPage.set((response as any).lastPage);
          this.page.set(page);
        },
        (error) => {
          if (this.page() === 1 && searchString === '') {
            this.error.set(error.error?.message ?? 'Unknown error occured');
          } else {
            this._snackbarService.showErrorSnackbar(
              error.error?.message ?? 'Unknown error occured',
            );
          }
          this.isFetchLoading.set(false);
        },
        () => {
          this.isFetchLoading.set(false);
        },
      );
  }

  goToPage(page: number, searchString: string = '') {
    const params: any = {
      page: page,
    };
    if (searchString.length > 0) {
      params.searchString = searchString;
    }
    this._router.navigate(['.'], {
      relativeTo: this._activatedRoute,
      queryParams: params,
    });
  }
}
