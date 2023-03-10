import { Component, OnInit, Input } from '@angular/core';
import { ModalService } from 'src/app/services/modal.service';
import { UserService } from 'src/app/services/user.service';
import { UserFormComponent } from '../user-form/user-form.component';
import { User } from '../user.model';

@Component({
  selector: 'app-user-item',
  templateUrl: './user-item.component.html',
  styleUrls: ['./user-item.component.scss'],
})
export class UserItemComponent implements OnInit {
  @Input() user: User;
  selected: boolean;

  constructor(
    private userService: UserService,
    private modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.userService.userSelected.subscribe((user: User) => {
      this.selected = this.user.id === user.id;
    });
  }

  onUserClicked() {
    this.userService.setCurrentUser(this.user);
  }

  onEditClicked() {
    this.userService.setCurrentUser(this.user);
    this.modalService.setModalContent({
      component: UserFormComponent,
      data: {
        isEdit: true,
      },
    });
    this.modalService.modalIsVisibleChanged.next(true);
  }
}
