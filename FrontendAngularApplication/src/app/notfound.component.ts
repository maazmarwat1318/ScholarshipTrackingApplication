import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'not-found',
  template: ` <div
    class="min-vh-100 p-3 bg-danger d-flex justify-content-center align-items-center"
    style="--bs-bg-opacity: 0.2"
  >
    <div class="rounded-3 shadow bg-white p-3 p-md-4">
      <div class="mb-4">
        <i
          class="bi bi-exclamation-triangle-fill not-found-icon display-5 text-danger"
        ></i>
      </div>
      <h1 class="fw-bold text-danger h2">404 - Page Not Found</h1>
      <p class="lead fw-medium text-muted mb-4">
        The page you're looking for doesn't exist or has been moved.
      </p>
      <div class="d-flex gap-3 justify-content-center">
        <button class="btn btn-outline-secondary" onclick="history.back()">
          <i class="bi bi-arrow-left-circle"></i> Go Back
        </button>
        <a href="/" class="btn btn-primary">
          <i class="bi bi-house-door-fill"></i> Home
        </a>
      </div>
    </div>
  </div>`,
})
export class NotFoundComponent {
  constructor(private _pageTitle: Title) {
    _pageTitle.setTitle('404 Not Found');
  }
}
