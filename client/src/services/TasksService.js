class TasksService {
	getAllTasks = () => {

		return new Promise((resolve, reject) => {
			resolve([
				{
					id: 1,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 0.009),
					type: "Signs and Layout"
				},
				{
					id: 2,
					name: 'Convince my team mates not to hate me for changing soo much stuff in the project',
					deadline: new Date(new Date().getTime() + 86400000 * 3.1),
					type: "Signs and Layout"
				},
				{
					id: 3,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 3.7),
					type: "Signs and Layout"
				},
				{
					id: 4,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 0.1),
					type: "Signs and Layout"
				},
				{
					id: 5,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 1.2),
					type: "Signs and Layout"
				},
				{
					id: 6,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 4.2),
					type: "Signs and Layout"
				},
				{
					id: 7,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 1.8),
					type: "Signs and Layout"
				},
				{
					id: 8,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * -3.4),
					type: "Signs and Layout"
				},
				{
					id: 9,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 3.8),
					type: "Signs and Layout"
				},
				{
					id: 10,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					id: 11,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					id: 12,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					id: 13,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					id: 14,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					id: 15,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					id: 16,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 3),
					type: "Signs and Layout"
				},
				{
					id: 17,
					name: 'Upload food stall logo',
					deadline: new Date(new Date().getTime() + 86400000 * 3),
					type: "Signs and Layout"
				}
			]);
		})
	}
}

export default new TasksService()