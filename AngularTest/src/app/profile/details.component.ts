import { Component } from '@angular/core';

import { AccountService } from '@app/_services';
import { Role} from './../_models';

@Component({ templateUrl: 'details.component.html' })
export class DetailsComponent {
    Role = Role;
    account = this.accountService.accountValue;

    constructor(private accountService: AccountService) { }
}