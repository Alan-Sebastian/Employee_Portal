import { NgModule } from "@angular/core";
import { HeaderComponent } from "./header/header.component";

import { FooterComponent } from "./footer/footer.component";
import { LayoutComponent } from "./Layout.component";
import { HomeComponent } from './home/home.component';
import { LayoutRoutingModule } from "./Layout-routing.module";
import { RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { LoginComponent } from "./login/login.component";
import { CommonModule } from "@angular/common";




@NgModule({
    declarations:[
        HeaderComponent,
        FooterComponent,
        LayoutComponent,
        HomeComponent,
        LoginComponent
        
  
    ],
    imports:[
        CommonModule,
        RouterModule,
        FormsModule,
        LayoutRoutingModule
    ],
    exports:[
        HeaderComponent,
        FooterComponent,
    LayoutComponent
    
    ]
})

export class LayoutModule{


}