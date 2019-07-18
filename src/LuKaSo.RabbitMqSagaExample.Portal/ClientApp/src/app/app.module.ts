import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AppMaterialModule } from './app-material.module';

import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { LoaderIndicatorComponent } from './components/loader-indicator/loader-indicator.component';
import { ChartsModule } from 'ng2-charts';
import { LoaderInterceptorService } from './components/loader-indicator/loader-interceptor.service';
import { LoaderIndicatorService } from './components/loader-indicator/loader-indicator.service';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoaderIndicatorComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    AppMaterialModule,
    ChartsModule
  ],
  exports: [
    BrowserAnimationsModule
  ],
  providers: [
    LoaderIndicatorService,
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptorService, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
