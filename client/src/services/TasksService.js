class TasksService {
	getAllTasks = () => {

		return new Promise((resolve, reject) => {
			resolve({
				data: [
					{
						id: 1,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 0.009),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 2,
						name: 'Convince my team mates not to hate me for changing soo much stuff in the project',
						deadline: new Date(new Date().getTime() + 86400000 * 3.1),
						type: {
							id: 1,
							name: "Redemption",
							color: "#218838"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 3,
						name: 'Provide Power Usage',
						deadline: new Date(new Date().getTime() + 86400000 * 3.7),
						type: {
							id: 1,
							name: "Supply",
							color: "#C82333"
						},
						description: `<p>We want to be sure that we can provide sufficient power supply for you and your kitchen. 
						We need to know how much power your machinery will require. Please provide the information in the table bellow</p>`
					},
					{
						id: 4,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 0.1),
						type: {
							id: 1,
							name: "Hawaii Blue",
							color: "#007bff"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 5,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 1.2),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 6,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 4.2),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 7,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 1.8),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 8,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * -3.4),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 9,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 3.8),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 10,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 3),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 11,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 3),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 12,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 3),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 13,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 3),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 14,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 3),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 15,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 3),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 16,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 3),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					},
					{
						id: 17,
						name: 'Upload food stall logo',
						deadline: new Date(new Date().getTime() + 86400000 * 3),
						type: {
							id: 1,
							name: "Signs and layouts",
							color: "#A000FF"
						},
						description: `<p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Deserunt, doloremque. Hic dolorem amet
						placeat. Nihil nobis aut earum, commodi iste accusamus modi sed recusandae, dolorum velit corporis
						aspernatur ullam placeat.</p>`
					}
				]
			});
		})
	}
}

export default new TasksService()