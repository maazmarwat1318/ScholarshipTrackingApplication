import { Component, computed, Signal } from '@angular/core';
import { UserService } from '../Services/UserService';
import { Title } from '@angular/platform-browser';
import { TitleService } from './Service/TitleService';

@Component({
  selector: 'moderator-component',
  templateUrl: './moderator.component.html',
  standalone: false,
})
export class ModeratorLayoutComponent {
  readonly title = computed<string>(() => this._titleService.title());
  constructor(public _titleService: TitleService) {}
}
