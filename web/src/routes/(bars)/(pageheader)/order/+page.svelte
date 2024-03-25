<script>
	import PageLoader from '$lib/component/spinner/page-loader.svelte';
	import { getOrderPreview } from '$lib/core/order/request';
</script>

{#await getOrderPreview()}
	<PageLoader />
{:then response}
	{@const orderHistories = response.data}
	<table class="table">
		<thead>
			<th scope="col">Order Id</th>
			<th scope="col">Order Status</th>
			<th scope="col">Created Date</th>
		</thead>
		<tbody>
			{#each orderHistories as order}
				<tr>
					<td>{order.orderId}</td>
					<td>{order.orderStatus}</td>
					<td>{new Date(order.orderDate).toLocaleDateString()}</td>
				</tr>
			{/each}
		</tbody>
	</table>
{/await}
