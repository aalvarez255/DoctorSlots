import { Patient } from './Patient'

export class TakeSlot {
    public facilityId: string
    public start: string
    public end: string
    public comments: string
    public patient: Patient
}
