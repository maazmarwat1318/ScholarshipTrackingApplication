import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'moderator-side-nav',
  template: `<div class="d-flex flex-column gap-1">
    @for (route of routes; track $index) {
      @if (route.children !== undefined) {
        <div
          [class.bg-primary]="isActive(route.path)"
          style="--bs-bg-opacity: 0.05"
        >
          <div class="nav-item" style="cursor: pointer">
            <a
              data-bs-toggle="collapse"
              [attr.data-bs-target]="'#collapse-' + route.path"
              [attr.aria-expanded]="isActive(route.path)"
              aria-controls="collapse"
              class="nav-link parent-nav"
            >
              {{ route.name }}
              <i class="ms-auto bi bi-chevron-down"></i>
            </a>
          </div>
          <div
            class="collapse"
            [class.show]="isActive(route.path)"
            [attr.id]="'collapse-' + route.path"
          >
            @for (childRoute of route.children; track $index) {
              <moderator-side-nav-tile
                [name]="childRoute.name"
                [path]="route.path + '/' + childRoute.path"
              ></moderator-side-nav-tile>
            }
          </div>
        </div>
      } @else {
        <moderator-side-nav-tile
          [name]="route.name"
          [path]="route.path"
        ></moderator-side-nav-tile>
      }
    }
  </div>`,
  standalone: false,
})
export class ModeratorSideNav {
  routes = [
    {
      name: 'Students',
      path: 'students',
      children: [
        {
          name: 'View Students',
          path: 'view',
        },
        {
          name: 'Create Student',
          path: 'create',
        },
      ],
    },
  ];

  constructor(private router: Router) {}

  isActive(prefix: string): boolean {
    const currentUrl = this.router.url;
    return currentUrl.startsWith('/moderator/' + prefix);
  }
}
