import { Component, Input, ViewChild } from '@angular/core';
import { ModalContentComponent } from './modal-content.component';
import { ModalContentDirective } from './modal-content.directive';
import { ModalContent } from './modal-content.modal';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'],
})
export class ModalComponent {
  @Input() content: ModalContent;

  @ViewChild(ModalContentDirective, { static: true })
  modalContent!: ModalContentDirective;

  ngOnInit() {
    this.initContent();
  }

  private initContent() {
    const viewContainerRef = this.modalContent.viewContainerRef;
    const componentRef =
      viewContainerRef.createComponent<ModalContentComponent>(
        this.content.component
      );
    componentRef.instance.data = this.content.data;
  }
}
