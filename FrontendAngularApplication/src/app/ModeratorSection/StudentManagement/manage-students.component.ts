import {
  Component,
  computed,
  effect,
  input,
  OnInit,
  signal,
} from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TitleService } from '../Service/TitleService';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../../Services/UserService';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'manage-students',
  template: `
    @if (isFetchLoading() && page() === 0) {
      <div
        class="min-full-height-inside-main-layout d-flex align-items-center justify-content-center"
      >
        <div
          class="spinner-border text-success"
          style="width: 50px; height: 50px;"
        ></div>
      </div>
    } @else {
      <div class="d-flex justify-content-end align-items-center mb-3 gap-2">
        <student-search-field (searchClicked)="this.goToPage(1, $event)" />
        <button class="btn btn-success">+ Create Student</button>
      </div>
      <student-table
        [students]="students()"
        [pageSize]="pageSize"
        [currentPage]="page()"
      ></student-table>
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
export class ManageStudentsComponent implements OnInit {
  students = signal<any[]>([]);
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
    private _userService: UserService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
  ) {
    _activatedRoute.queryParams.subscribe((params) => {
      let page = Number(params['page']);
      let pageSize = Number(params['pageSize']);
      let searchString = params['searchString'];
      if (!isNaN(page) && page > 0) this.page.set(page);
      if (!isNaN(pageSize) && pageSize > 0) this.pageSize = pageSize;
      if (searchString !== null) this.searchString.set(params['searchString']);
      this.getStudents(this.page(), this.searchString());
    });
  }

  ngOnInit() {
    this._titleService.setTitle('Manage Students');
  }

  getStudents(page: number, searchString: string = '') {
    this.isFetchLoading.set(true);
    this._httpClient
      .get('https://localhost:7222/Student', {
        params: {
          page: page,
          searchString: searchString,
          pageSize: this.pageSize,
        },
        headers: { Authorization: `Bearer ${this._userService.authToken!}` },
      })
      .subscribe(
        (response) => {
          this.students.set((response as any).students);
          this.isLastPage.set((response as any).lastPage);
          this.page.set(page);
        },
        (error) => {
          error = error.error;
        },
        () => {
          this.isFetchLoading.set(true);
        },
      );
  }

  goToPage(page: number, searchString: string = '') {
    const params: any = {
      page: page,
      pageSize: this.pageSize,
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
