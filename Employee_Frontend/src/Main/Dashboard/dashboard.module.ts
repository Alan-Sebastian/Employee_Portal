import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard.component';
import { EmployeeModalComponent } from './employee-modal/employee-modal.component';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [DashboardComponent,
    EmployeeModalComponent],
  imports: [
    CommonModule,
    FormsModule,
    DashboardRoutingModule,
   ReactiveFormsModule,
   HttpClientModule
  ]
})
export class DashboardModule { }
