class TasksService {
	getAllTasks = () => {

		return new Promise((resolve, reject) => {
			resolve([
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 0.009),
					type: "Signs and Layout"
				},
				{
					name: 'Convince my team mates not to hate me for changing soo much stuff in the project',
					deadline: new Date(1573396289643 + 86400000 * 3.1),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 3.7),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 5),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 1.2),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 4.2),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 1.8),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * -3.4),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 3.8),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					name: 'Upload food stall logo',
					deadline: new Date(1573396289643 + 86400000 * 3),
					type: "Signs and Layout"
				}
			]);
		})
	}
}

export default new TasksService()