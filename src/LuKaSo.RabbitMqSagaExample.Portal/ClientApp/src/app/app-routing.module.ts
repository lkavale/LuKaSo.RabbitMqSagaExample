import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SelectivePreloadingStrategyService } from '../app/services/selective-preloading-strategy.service';
import { StrategyConfigFormComponent } from './modules/strategy-config-form/strategy-config-form.component';
import { AppMaterialModule } from './app-material.module';
import { StrategyConfigComponent } from './components/strategy-config/strategy-config.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { StrategyApiProviderService } from './services/strategy-api-provider.service';
import { InvestmentFormComponent } from './modules/investment-form/investment-form.component';

import { OrderManagementConfigFormComponent } from './modules/order-management-config-form/order-management-config-form.component';
import { OrderManagementConfigComponent } from './components/order-management-config/order-management-config.component';
import { InvestmentComponent } from './components/investment/investment.component';
import { InvestmentListComponent } from './components/investment-list/investment-list.component';
import { InvestmentService } from './services/investment.service';
import { PortfolioService } from './services/portfolio.service';
import { OrderManagementService } from './services/order-management.service';
import { ChartsModule } from 'ng2-charts';

const appRoutes: Routes = [
  {
    path: 'investment',
    component: InvestmentFormComponent
  },
  {
    path: 'orderManagement',
    component: OrderManagementConfigFormComponent
  },
  {
    path: 'strategyA',
    component: StrategyConfigFormComponent,
    data: {
      strategyName: 'Strategy A',
      strategyAddress: 'strategyA'
    }
  },
  {
    path: 'strategyB',
    component: StrategyConfigFormComponent,
    data: {
      strategyName: 'Strategy B',
      strategyAddress: 'strategyB'
    }
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
      {
        enableTracing: false, // <-- debugging purposes only
        preloadingStrategy: SelectivePreloadingStrategyService,
      }
    ),
    AppMaterialModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    ChartsModule
  ],
  exports: [
    RouterModule,
    AppMaterialModule
  ],
  declarations: [
    StrategyConfigComponent,
    StrategyConfigFormComponent,
    OrderManagementConfigComponent,
    OrderManagementConfigFormComponent,
    InvestmentFormComponent,
    InvestmentComponent,
    InvestmentListComponent
  ],
  providers: [
    StrategyApiProviderService,
    OrderManagementService,
    InvestmentService,
    PortfolioService
  ]
})
export class AppRoutingModule { }
