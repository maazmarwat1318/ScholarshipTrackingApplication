import { Component, input } from '@angular/core';

@Component({
  selector: 'moderator-side-nav-tile',
  template: ` <div class="nav-item">
    <a class="nav-link" [routerLink]="path()" routerLinkActive="active">
      <span [class.ms-3]="isChild()">{{ name() }}</span>
    </a>
  </div>`,
  standalone: false,
})
export class ModeratorSideNavTile {
  name = input.required<string>();
  path = input.required<string>();
  isChild = input(true);
}
