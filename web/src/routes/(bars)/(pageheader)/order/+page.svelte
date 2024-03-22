<script>
	import { getOrderPreview } from '$lib/order/request';
	import { onMount } from 'svelte';

	const getOrderPreviewTask = getOrderPreview();

	/**
	 * @type {any[]}
	 */
	let orders = [];

	onMount(async () => {
		const response = await getOrderPreviewTask;

		orders = [...response.data];
	});
</script>

<table class="table">
	<thead>
		<th scope="col">Order Id</th>
		<th scope="col">Order Status</th>
		<th scope="col">Created Date</th>
	</thead>
	<tbody>
		{#each orders as order}
			<tr>
				<td>{order.orderId}</td>
				<td>{order.orderStatus}</td>
				<td>{new Date(order.orderDate).toLocaleDateString()}</td>
			</tr>
		{/each}
	</tbody>
</table>
