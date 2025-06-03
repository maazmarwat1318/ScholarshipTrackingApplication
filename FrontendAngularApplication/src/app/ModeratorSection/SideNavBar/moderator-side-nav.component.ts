import { Component } from '@angular/core';

@Component({
  selector: 'moderator-side-nav',
  template: ` <div class="flex-column gap-1">
    @for (route of routes; track $index) {
      <moderator-side-nav-tile
        [name]="route.name"
        [path]="route.path"
      ></moderator-side-nav-tile>
    }
  </div>`,
  standalone: false,
})
export class ModeratorSideNav {
  routes = [
    {
      name: 'Manage Students',
      path: '/moderator/students',
    },
  ];
}
