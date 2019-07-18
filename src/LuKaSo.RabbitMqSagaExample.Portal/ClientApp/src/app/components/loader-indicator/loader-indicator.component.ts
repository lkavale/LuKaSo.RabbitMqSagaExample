import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { LoaderIndicatorService } from './loader-indicator.service';
import { LoaderIndicatorState } from './loader-indicator';

@Component({
  selector: 'loader-indicator',
  templateUrl: './loader-indicator.component.html',
  styleUrls: ['./loader-indicator.component.css']
})
export class LoaderIndicatorComponent implements OnInit {
  visible = false;
  private subscription: Subscription;

  constructor(private loaderIndicatorService: LoaderIndicatorService) {
  }

  ngOnInit() {
    this.subscription = this.loaderIndicatorService.loaderIndicatorState
      .subscribe((state: LoaderIndicatorState) => {
        this.visible = state.visible;
      });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
