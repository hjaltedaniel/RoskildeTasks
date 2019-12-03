import TaskslistComponent from '../../src/components/TasksList';

class MockTasksService {
	getAllTasks = jest.fn(() => {
		return new Promise((resolve, reject) => { resolve([])})
	})
}

var component;

beforeEach(() => {
	component = TaskslistComponent;
	component.tasksService = new MockTasksService();
})

describe('TaskslistComponent', () => {
	describe('getTimeToDeadline', () => {
		it('should return the number of minutes until deadline in the format `+{nrOfMinutes} minutes`', () => {
			const result = component.methods.getTimeToDeadline(new Date(new Date().getTime() + 86400000 * 0.009));
			expect(result).toBe("+13 minutes");
		})
		it('should return the number of days until deadline in the format `+{nrOfDays} days`', () => {
			const result = component.methods.getTimeToDeadline(new Date(new Date().getTime() + 86400000 * 3));
			expect(result).toBe("+3 days");
		})
		it('should return "Overdue"', () => {
			const result = component.methods.getTimeToDeadline(new Date(new Date().getTime() + 86400000 * -0.001));
			expect(result).toBe("Overdue");
		})
	})
})