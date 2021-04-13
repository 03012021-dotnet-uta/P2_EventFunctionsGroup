import { Component } from '@angular/core';
import { AccountService} from './../_services';

@Component({ templateUrl: 'layout.component.html' })
export class LayoutComponent {
    account = this.accountService.accountValue;
    constructor(private accountService: AccountService) {
                
    }
    isEventManager(){
        if (this.account.isEventManager)
            return true;
        else
            return false;
    }
 }